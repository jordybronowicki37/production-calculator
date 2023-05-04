import {Editor} from "../components/editor/Editor";
import {useDispatch, useSelector} from "react-redux";
import {useEffect, useState} from "react";
import {fetchProject} from "../data/api/ProjectsAPI";

export function EditorPage(props) {
  const projectId = props.match.params.id;
  const project = useSelector(state => state.project);
  const dispatch = useDispatch();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchProject(projectId).then(() => setLoading(false));
    
    return () => {
      dispatch({type:"unload_worksheet"});
    }
  }, []);
  
  return <>
    {loading
      ? <div>Loading</div>
      : <Editor project={project}/>
    }
  </>
}