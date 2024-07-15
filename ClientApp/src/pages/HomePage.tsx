import {Redirect} from "react-router-dom";

export function HomePage() {
  return (
      <div>
        <Redirect to="/projects"/>
      </div>
  );
}
