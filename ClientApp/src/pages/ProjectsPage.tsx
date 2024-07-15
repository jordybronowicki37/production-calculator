import "./ProjectsPage.scss";
import {useEffect, useState} from "react";
import {useSelector} from "react-redux";
import {fetchAllProjects} from "../data/api/ProjectsAPI";
import {Button, Spinner} from "reactstrap";
import {Popup} from "../components/popup/Popup";
import {ProjectCreator} from "../components/project/ProjectCreator";
import {StoreStates} from "../data/DataStore";
import {ProjectItem} from "../components/project/ProjectItem";
import {ProjectDto} from "../data/api/ApiDtoTypes";

export function ProjectsPage() {
  const projects = useSelector<StoreStates, StoreStates["projects"]>(state => state.projects);
  const [isLoading, setIsLoading] = useState(true);
  const [projectCreatorOpen, setProjectCreatorOpen] = useState(false);
  
  useEffect(() => {
    fetchAllProjects().then(() => setIsLoading(false));
  }, []);
  
  return (
    <div className="project-page">
      <Popup 
        onClose={() => setProjectCreatorOpen(false)}
        hidden={!projectCreatorOpen}>
        <ProjectCreator onClose={() => setProjectCreatorOpen(false)}/>
      </Popup>
      <div className="project-header">
        <h1>Your projects</h1>
        
        <Button 
          className="project-create-button" color="light" outline
          onClick={() => setProjectCreatorOpen(true)}>
          Create a new project
        </Button>
      </div>
      
      {!isLoading && <ProjectItems projects={projects}/>}
      {!isLoading && projects.length === 0 && <NoProjectYet/>}
      {isLoading && <ProjectsLoading/>}
    </div>
  );
}

function ProjectItems({projects}: { projects: ProjectDto[] }) {
    return <div className="project-items-container">
        {projects.map(p => <ProjectItem key={p.id} project={p}/>)}
    </div>
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
