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
                {
                    Object.values(SectionType).map((val) =>
                        <BoardsSection boards={boards} type={val} key={val} />)
                }
            </div>
        );
    }
}

export default BoardsPage;