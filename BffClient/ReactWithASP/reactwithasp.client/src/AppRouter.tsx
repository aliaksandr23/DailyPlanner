import HomePage from "./pages/HomePage";
import ErrorPage from "./pages/ErrorPage";
import BoardPage from "./pages/BoardPage";
import RootLayout from "./pages/RootLayout";
import BoardsPage from "./pages/BoardsPage";
import { createBrowserRouter } from "react-router-dom";
import CardModalPage from "./pages/CardDetailsModalPage";

export const AppRouter = createBrowserRouter([
    {
        element: <RootLayout />,
        errorElement: <ErrorPage />,
        children: [
            {
                path: "/",
                element: <HomePage />
            },
            {
                path: "/boards",
                element: <BoardsPage />
            },
            {
                path: "/board/:boardId",
                element: <BoardPage />,
                children: [
                    {
                        path: "card/:cardId",
                        element: <CardModalPage />
                    }
                ]
            }
        ],
    },
]);

export default AppRouter;