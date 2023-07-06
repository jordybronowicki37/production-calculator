import "./ProjectsPage.scss";
import {useEffect, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {fetchAllProjects} from "../data/api/ProjectsAPI";
import {Link} from "react-router-dom";
import {Button, Spinner} from "reactstrap";
import {WorksheetItem} from "../components/worksheets/WorksheetItem";

export function ProjectsPage() {
  const projects = useSelector(state => state.projects);
  const [isLoading, setIsLoading] = useState(true);
  
  useEffect(() => {
    fetchAllProjects().then(() => setIsLoading(false));
  }, []);
  
  return (
    <div className="project-page">
      <div className="project-header">
        <h1>Your projects</h1>
        <Button className="project-create-button" color="light" outline>Create a new project</Button>
      </div>
      {!isLoading && projects.map(p => ProjectItem(p))}
      {!isLoading && projects.length === 0 && <NoProjectYet/>}
      {isLoading && <ProjectsLoading/>}
    </div>
  );
}

function ProjectItem(project) {
  const {id, name, worksheets, products} = project;
  
  return (
    <div key={id} className="project-item">
      <div className="project-top">
        <Link to={`project/${id}`} className="project-link">{name}</Link>
        <Link to={`editor/${id}`} className="project-link"> > Editor</Link>
        <ProjectStats project={project}/>
      </div>
      <div className="project-worksheets">
        {worksheets.map(w => <WorksheetItem key={w.id} worksheet={w} products={products}/>)}
      </div>
    </div>
  );
}

function ProjectsLoading() {
  return (
    <div className="projects-loading-page">
      <Spinner color="light"/>
      <h2>Loading your projects</h2>
    </div>
  );
}

function NoProjectYet() {
  return (
    <div className="no-projects-yet-page">
      <h2>You don't have any projects yet</h2>
      <Button className="project-create-button" color="primary" outline>Create a new project</Button>
    </div>
  );
}

function ProjectStats({project}) {
  return (
    <div className="project-stats">
      <div>
        <span className="material-symbols-rounded" title="Products">pie_chart</span>
        {project.products.length}
      </div>
      <div>
        <span className="material-symbols-rounded" title="Recipes">account_tree</span>
        {project.recipes.length}
      </div>
      <div>
        <span className="material-symbols-rounded" title="Machines">precision_manufacturing</span>
        {project.machines.length}
      </div>
      <div>
        <span className="material-symbols-rounded" title="Worksheets">table_chart</span>
        {project.worksheets.length}
      </div>
    </div>
  );
}
