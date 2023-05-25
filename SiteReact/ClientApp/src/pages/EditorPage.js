import {Editor} from "../components/editor/Editor";
import {useDispatch, useSelector} from "react-redux";
import {useEffect, useState} from "react";
import {fetchProject} from "../data/api/ProjectsAPI";

export function EditorPage(props) {
  const projectId = props.match.params.id;
  const { project, worksheet, products, recipes, machines } = useSelector(state => state);
  const dispatch = useDispatch();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchProject(projectId).then(() => setLoading(false));
    
    return () => {
      dispatch({type:"unload_project"});
    }
  }, []);
  
  return <>
    {loading
      ? <div>Loading</div>
      : <Editor project={project} worksheet={worksheet} products={products} recipes={recipes} machines={machines}/>
    }
  </>
}