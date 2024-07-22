import {Redirect} from "react-router-dom";
import React from "react";

export function HomePage(): React.JSX.Element {
  return (
      <div>
        <Redirect to="/projects"/>
      </div>
  );
}
