import { Navigate } from "react-router-dom";
import { SectionType } from "../types/types";
import BoardsSection from "../components/BoardsSection";
import { Spinner } from "../components/UI/Spinner/Spinner";
import { useGetBoardsQuery } from "../redux/slices/apiSlice";

const BoardsPage: React.FC = () => {
    const {
        data: boards,
        isLoading,
        isSuccess,
        isError,
    } = useGetBoardsQuery();

    if (isLoading) {
        return (<Spinner />);
    }
    else if (isError) {
        return (<Navigate to="/error" replace />);
        
    }
    else if (isSuccess) {
        return (
            <div className="container">
                <BoardsSection
                    boards={boards}
                    type={SectionType.RecentlyViewed}
                />
                <BoardsSection
                    boards={boards}
                    type={SectionType.Favorite}
                />
                <BoardsSection
                    boards={boards}
                    type={SectionType.OwnBoards}
                />
            </div>
        );
    }
}

export default BoardsPage;