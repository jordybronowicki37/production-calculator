import "./ProjectsPage.css";
import {useEffect, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {fetchAllProjects} from "../data/api/ProjectsAPI";
import {Link} from "react-router-dom";
import {Button} from "reactstrap";
import {WorksheetItem} from "../components/worksheets/WorksheetItem";

export function ProjectsPage() {
  const projects = useSelector(state => state.projects);
  const dispatch = useDispatch();
  
  useEffect(() => {
    fetchAllProjects();
  }, []);
  
  return (
    <div>
      {projects.map(p => ProjectItem(p))}
      {projects.length === 0 && <NoProjectYet/>}
    </div>
  );
}

function ProjectItem(project) {
  const {id, name, worksheets, products} = project;
  
  return (
    <div key={id} className="project_item">
      <div className="project_top">
        <Link to={`project/${id}`} className="project_link">{name}</Link>
        <Link to={`editor/${id}`} className="project_link"> > Editor</Link>
        <ProjectStats project={project}/>
      </div>
      <div className="project_worksheets">
        {worksheets.map(w => <WorksheetItem key={w.id} worksheet={w} products={products}/>)}
      </div>
    </div>
  );
}

function NoProjectYet() {
  return (
    <div className="project_none">
      <h2>You don't have any projects yet</h2>
      <Button className="project_create_button" color="primary" outline>Create a new project</Button>
    </div>
  );
}

function ProjectStats({project}) {
  return (
    <div className="project_stats">
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
