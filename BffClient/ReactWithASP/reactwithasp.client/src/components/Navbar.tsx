import { Link } from "react-router-dom";
import { useAppSelector } from "../redux/hooks/authHooks";

const Navbar: React.FC = () => {
    const { isAuthorized, loginUrl, logoutUrl } = useAppSelector(state => state.auth);

    return (
        <nav className="navbar container">
            <div className="brand-section">
                <Link to="/" className="nav-title link">DailyPlanner</Link>
            </div>
            <div className="nav-section">
                <ul className="nav-list">
                    <li className="nav-item">
                        {isAuthorized
                            ? <a href={logoutUrl} className="nav-link">Log out</a>
                            : <a href={loginUrl} className="nav-link">Log In</a>
                        }
                    </li>
                    {isAuthorized && (
                        <li className="nav-item">
                            <Link to="/boards" className="nav-link">My boards</Link>
                        </li>
                    )}
                </ul>
            </div>
        </nav>
    );
}

export default Navbar;