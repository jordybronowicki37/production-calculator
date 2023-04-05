import "./ProjectPage.css";
import {useEffect} from "react";
import {fetchProject} from "../data/api/ProjectsAPI";
import {useDispatch, useSelector} from "react-redux";
import {WorksheetOverview} from "../components/worksheets/WorksheetOverview";

export function ProjectPage(props) {
  const projectId = props.match.params.id;
  const project = useSelector(state => state.project);
  const dispatch = useDispatch();

  useEffect(() => {
    fetchProject(projectId);
  }, []);
  
  return (
    <div>
      <WorksheetOverview worksheets={project?project.worksheets:[]}/>
    </div>
  );
}
