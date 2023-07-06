import "./WorksheetCreator.scss";
import {Button, Spinner} from "reactstrap";
import {createNewWorksheet} from "../../data/api/WorksheetAPI";
import {useState} from "react";

export function WorksheetCreator({handleResult, projectId}) {
  const [newWorksheetName, setNewWorksheetName] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const [newWorksheetNameError, setNewWorksheetNameError] = useState(false);
  
  return (
    <div className="worksheet-creator">
      <div className="header"><h2 className="m-0">New Worksheet</h2></div>
      <div className="content">
        <div>
          <label htmlFor="new-worksheet-name">Worksheet name</label>
          <input
            id="new-worksheet-name"
            value={newWorksheetName}
            onChange={e => {
              setNewWorksheetNameError(false);
              setNewWorksheetName(e.target.value);
            }}/>
        </div>

        {newWorksheetNameError && <span className="error-message">Worksheet name can not be empty!</span>}
        
        <span>
          <Button 
            type="submit"
            color={newWorksheetNameError ? "danger" : "light"}
            outline 
            className="create-worksheet-button"
            onClick={(e) => {
              e.preventDefault();
              if (isLoading) return;
              
              if (newWorksheetName === "") {
                setNewWorksheetNameError(true);
                return;
              }
              
              setIsLoading(true);
              createNewWorksheet(projectId, newWorksheetName).then(handleResult).catch(() => setIsLoading(false));
            }}
          >
            Create
          </Button>
          {isLoading && <Spinner color="light" size="sm"/>}
        </span>
      </div>
    </div>
  );
}