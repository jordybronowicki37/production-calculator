import {Editor} from "../components/editor/Editor";
import {useDispatch, useSelector} from "react-redux";
import React, {useEffect, useState} from "react";
import {fetchProject} from "../data/api/ProjectsAPI";
import {ProjectUnloadAction} from "../data/reducers/ProjectReducer";
import {StoreStates} from "../data/DataStore";

export type EditorPageProps = {
  match: {
    params: {
      id: string
    }
  }
}

export function EditorPage(props: EditorPageProps): React.JSX.Element {
  const projectId: string = props.match.params.id;
  const project = useSelector<StoreStates, StoreStates["project"]>(state => state.project);
  const worksheets = useSelector<StoreStates, StoreStates["worksheets"]>(state => state.worksheets);
  const products = useSelector<StoreStates, StoreStates["products"]>(state => state.products);
  const recipes = useSelector<StoreStates, StoreStates["recipes"]>(state => state.recipes);
  const machines = useSelector<StoreStates, StoreStates["machines"]>(state => state.machines);
  const dispatch = useDispatch();
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    fetchProject(projectId).then(() => setLoading(false));

    return () => {
      dispatch(ProjectUnloadAction());
    }
  }, [projectId, dispatch]);

  return <>
    {loading
      ? <div>Loading</div>
      : <Editor project={project} worksheets={worksheets} products={products} recipes={recipes} machines={machines}/>
    }
  </>
}