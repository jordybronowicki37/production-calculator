import {Editor} from "../components/editor/Editor";
import {useDispatch, useSelector} from "react-redux";
import {useEffect, useState} from "react";
import {fetchProject} from "../data/api/ProjectsAPI";
import {ProjectUnloadAction} from "../data/reducers/ProjectReducer";
import {StoreStates} from "../data/DataStore";

export function EditorPage(props) {
  const projectId: string = props.match.params.id;
  const { project, worksheets, products, recipes, machines } = 
      useSelector<StoreStates, StoreStates>(state => state);
  const dispatch = useDispatch();
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    fetchProject(projectId).then(() => setLoading(false));
    
    return () => {
      dispatch(ProjectUnloadAction());
    }
  }, []);
  
  return <>
    {loading
      ? <div>Loading</div>
      : <Editor project={project} worksheets={worksheets} products={products} recipes={recipes} machines={machines}/>
    }
  </>
}