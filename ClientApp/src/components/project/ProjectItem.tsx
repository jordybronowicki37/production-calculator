import "./ProjectItem.scss";
import {ProjectDto} from "../../data/api/ApiDtoTypes";
import {Link} from "react-router-dom";

export function ProjectItem({project}:{project: ProjectDto}) {
    const {id, name, entityContainer} = project;
    
    return (
        <div key={id} className="project-item">
            <div className="project-top">
                <img src={GetPictureLink(entityContainer.name)} alt="Project preset type"/>
                <Link to={`editor/${id}`} className="project-link">{name}</Link>
            </div>
            <p className="entity-container-name">Entities from: {entityContainer.name}</p>
            <ProjectStats project={project}/>
            <Link to={`editor/${id}`} className="load-project-button">Load project</Link>
        </div>
    );
}

function ProjectStats({project}:{project: ProjectDto}) {
    return (
        <div className="project-stats">
            <ProjectStatsItem title="Worksheets" stat={project.worksheets.length}/>
            <ProjectStatsItem title="Products" stat={project.entityContainer.products.length}/>
            <ProjectStatsItem title="Recipes" stat={project.entityContainer.recipes.length}/>
            <ProjectStatsItem title="Machines" stat={project.entityContainer.machines.length}/>
        </div>
    );
}

function ProjectStatsItem({title, stat}: { title: string, stat: any }) {
    return (
        <div className="project-stats-item">
            <p>{title}</p>
            <p>{stat}</p>
        </div>
    )
}

function GetPictureLink(name: string): string {
    switch (name) {
        case "Dyson Sphere Program":
            return "https://upload.wikimedia.org/wikipedia/en/0/07/Dyson_Sphere_Program_cover.jpg";
        case "Factorio":
            return "https://cdn.akamai.steamstatic.com/steam/apps/427520/header.jpg";
        case "Satisfactory Early Access":
            return "https://clan.cloudflare.steamstatic.com/images/32967947/f97d88a1f171dd3986298c03612cd8f83b081128.png";
        case "Satisfactory Experimental":
            return "https://clan.cloudflare.steamstatic.com/images/32967947/60043e945f10c721a8e02fb3fd9b767c3761e54f.png";
        case "Satisfactory FICSMAS":
            return "https://preview.redd.it/ficsmas-2022-starts-in-1-week-see-sticky-comment-v0-4e57clmr0x1a1.png?auto=webp&s=c1d1d79940714734bb0a0a19f0ccbeec092c76cd";
        default:
            return "https://www.nbmchealth.com/wp-content/uploads/2018/04/default-placeholder.png";
    }
}
