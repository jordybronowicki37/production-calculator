import './NavMenu.css';
import {useState} from 'react';
import {Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';

export function NavMenu() {
  const [collapsed, setCollapsed] = useState<boolean>(true);
  
  return (
      <header>
        <Navbar className="navbar-expand-sm navbar-dark navbar-toggleable-sm border-bottom box-shadow" color="dark">
          <NavbarBrand tag={Link} to="/">Production calculator</NavbarBrand>
          <NavbarToggler onClick={() => setCollapsed(!collapsed)} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} to="/">Home</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} to="/projects">Projects</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="https://github.com//jordybronowicki37/productionCalculator">
                  Github
                  <span className="material-symbols-rounded float-end fs-6">open_in_new</span>
                </NavLink>
              </NavItem>
            </ul>
          </Collapse>
        </Navbar>
      </header>
  );
}
