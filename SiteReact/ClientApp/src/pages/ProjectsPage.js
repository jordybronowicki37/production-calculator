import "./ProjectsPage.css";
import {useEffect} from "react";
import {useDispatch, useSelector} from "react-redux";
import {fetchAllProjects} from "../data/api/ProjectsAPI";
import {Link} from "react-router-dom";

export function ProjectsPage() {
  const projects = useSelector(state => state.projects);
  const dispatch = useDispatch();
  
  useEffect(() => {
    fetchAllProjects();
  }, []);
  
  return (
    <div>
      {projects.map(ProjectItem)}
      {projects.length === 0 && <NoProjectYet/>}
    </div>
  );
}

function ProjectItem(project) {
  const {id, name, amountWorksheets} = project;
  return <div key={id} className="projects_item">
    <Link to={`project/${id}`}>{name}</Link>
    <div>{amountWorksheets}</div>
  </div>
}

function NoProjectYet() {
  return (
    <div className="projects_none">
      <div>You don't have any projects yet</div>
      <button>Create a project</button>
    </div>
  );
}
