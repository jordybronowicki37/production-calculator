import {useEffect, useState} from "react";
import {AxiosResponse, isCancel} from "axios";

export type UseRequestBaseProps<RequestBodyType, ResponseBodyType> = {
  action: (params: RequestBodyType) => Promise<AxiosResponse<ResponseBodyType>>,
  abortController: AbortController;
  handleResponse?: (response: AxiosResponse<ResponseBodyType>) => void;
}

export type UseSendRequestBaseType<RequestBodyType, ResponseBodyType> = {
  data: ResponseBodyType | null,
  error: Error | null,
  loading: boolean,
  triggerRequest: (params: RequestBodyType) => Promise<ResponseBodyType>
}

export function useSendRequestBase<RequestBodyType, ResponseBodyType>(props: UseRequestBaseProps<RequestBodyType, ResponseBodyType>): UseSendRequestBaseType<RequestBodyType, ResponseBodyType> {
  const [data, setData] = useState<ResponseBodyType | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const triggerRequest = async (params: RequestBodyType) => {
    setLoading(true);
    try {
      const response = await props.action(params);
      if (props.handleResponse) props.handleResponse(response);
      setData(response.data);
      setError(null);
      return response.data;
    } catch (err) {
      if (isCancel(err)) {
        console.log('Request canceled', err.message);
      } else {
        setError(err as Error);
      }
      return Promise.reject(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    return () => {
      props.abortController.abort("Component unmounted");
    };
  }, [props.abortController]);

  return { data, error, loading, triggerRequest };
}

export type UseAutoFetchRequestBaseProps<ResponseBodyType> = {
  action: () => Promise<AxiosResponse<ResponseBodyType>>,
  abortController: AbortController;
  handleResponse?: (response: AxiosResponse<ResponseBodyType>) => void;
}

export type UseAutoFetchRequestBaseType<ResponseBodyType> = {
  data: ResponseBodyType | null,
  error: Error | null,
  loading: boolean,
}

export function useAutoFetchRequestBase<ResponseBodyType>(props: UseAutoFetchRequestBaseProps<ResponseBodyType>): UseAutoFetchRequestBaseType<ResponseBodyType> {
  const {action, abortController, handleResponse} = props;
  const [data, setData] = useState<ResponseBodyType | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    const triggerRequest = async () => {
      setLoading(true);
      try {
        const response = await action();
        if (handleResponse) handleResponse(response);
        setData(response.data);
        setError(null);
      } catch (err) {
        if (isCancel(err)) {
          console.log('Request canceled', err.message);
        } else {
          setError(err as Error);
        }
      } finally {
        setLoading(false);
      }
    };
    triggerRequest();
    
    return () => {
      abortController.abort();
    };
  }, [abortController, action, handleResponse]);

  return { data, error, loading };
}

export type UseApiRequestBaseType<ResponseType> = {
  data: ResponseType | null,
  error: Error | null,
  loading: boolean
}