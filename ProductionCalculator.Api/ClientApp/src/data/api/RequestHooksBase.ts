import {useEffect, useState} from "react";
import {AxiosResponse, isCancel} from "axios";

export type UseRequestBaseProps<RequestBodyType, ResponseBodyType> = {
  action: (params: RequestBodyType) => Promise<AxiosResponse<ResponseBodyType>>,
  abortController: AbortController;
  handleResponse?: (response: AxiosResponse<ResponseBodyType, any>) => void;
}

export function useSendRequestBase<RequestBodyType, ResponseBodyType>(props: UseRequestBaseProps<RequestBodyType, ResponseBodyType>) {
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
  }, []);

  return { data, error, loading, triggerRequest };
}

export type UseAutoFetchRequestBaseProps<ResponseBodyType> = {
  action: () => Promise<AxiosResponse<ResponseBodyType>>,
  abortController: AbortController;
  handleResponse?: (response: AxiosResponse<ResponseBodyType, any>) => void;
}

export function useAutoFetchRequestBase<ResponseBodyType>(props: UseAutoFetchRequestBaseProps<ResponseBodyType>) {
  const [data, setData] = useState<ResponseBodyType | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const triggerRequest = async () => {
    setLoading(true);
    try {
      const response = await props.action();
      if (props.handleResponse) props.handleResponse(response);
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

  useEffect(() => {
    return () => {
      props.abortController.abort();
    };
  }, []);

  return { data, error, loading };
}