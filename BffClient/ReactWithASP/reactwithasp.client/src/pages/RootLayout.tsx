import Navbar from "../components/Navbar";
import { Outlet } from "react-router-dom";

const RootLayout: React.FC = () => {

    return (
        <>
            <header>
                <Navbar />
            </header>
            <main>
                <Outlet />
            </main>
        </>
    );
}

export default RootLayout;