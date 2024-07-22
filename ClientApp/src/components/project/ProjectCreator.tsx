import "./ProjectCreator.scss";
import {Button, Spinner} from "reactstrap";
import React, {useState} from "react";
import {createProject} from "../../data/api/ProjectsAPI";

export function ProjectCreator({onClose}: { onClose: () => void }): React.JSX.Element {
  const [projectName, setProjectName] = useState<string>("");
  const [projectType, setProjectType] = useState<string>("");
  const [projectNameError, setProjectNameError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  return (
    <div className="project-creator">
      <div className="creator-header">
        <h2 className="p-2 m-0">New Project</h2>
      </div>
      <form className="creator-content">
        <div>
          <label htmlFor="new-project-name">Name</label>
          <input id="new-project-name" type="text" value={projectName}
                 onChange={(e) => {
                   setProjectNameError(false);
                   setProjectName(e.target.value);
                 }
                 }/>
        </div>

        <div>
          <label htmlFor="new-project-type">Type preset</label>
          <select id="new-project-type"
                  onChange={(e) => {
                    setProjectType(e.target.value);
                  }}
          >
            <option value="">None</option>
            <option value="dysonSphereProgram">Dyson Sphere Program</option>
            <option value="factorio">Factorio</option>
            <optgroup label="Satisfactory">
              <option value="satisfactoryEarlyAccess">Satisfactory Early Access</option>
              <option value="satisfactoryExperimental">Satisfactory Experimental</option>
              <option value="satisfactoryFICSMAS">Satisfactory FICSMAS</option>
            </optgroup>
          </select>
        </div>

        {projectNameError && <div className="error-message">Project name can not be empty!</div>}

        <span>
          <Button
            type="submit" outline
            color={projectNameError ? "danger" : "light"}
            className="create-project-button"
            onClick={(e) => {
              e.preventDefault();

              if (isLoading) return;
              if (projectName === "") {
                setProjectNameError(true);
                return;
              }

              setIsLoading(true);
              createProject(projectName, projectType).then(onClose);
            }}>
            Create
          </Button>
          {isLoading && <Spinner color="light" size="sm"/>}
        </span>
      </form>
    </div>
  );
}
