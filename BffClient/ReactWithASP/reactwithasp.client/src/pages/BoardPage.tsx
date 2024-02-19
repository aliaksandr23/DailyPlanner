import { useState } from "react";
import { Column } from "../types/types";
import ColumnItem from "../components/ColumnItem";
import { Navigate, useParams } from "react-router-dom";
import { Spinner } from "../components/UI/Spinner/Spinner";
import { useCreateColumnMutation, useGetBoardByIdQuery } from "../redux/slices/apiSlice";
import { IoStarOutline, IoLockClosedOutline, IoLockOpenOutline, IoClose } from "react-icons/io5";

const BoardPage: React.FC = () => {
    const { boardId } = useParams<{ boardId: string }>();
    const [isNewColumnDropdownOpen, setNewColumnDropdownOpen] = useState<boolean>(false);
    const togleDropdown = (state: boolean) => {
        setNewColumnDropdownOpen(state);
    }
    const [column, setColumn] = useState<Partial<Column>>({
        title: "",
        boardId: ""
    });
    const {
        data: board,
        isLoading,
        isSuccess,
    } = useGetBoardByIdQuery(boardId);
    const [createNewColumn] = useCreateColumnMutation();
    const onSubmitClicked = async (boardId: string) => {
        try {
            await createNewColumn({ ...column, boardId: boardId });
            setColumn({
                title: "",
                boardId: ""
            });
            togleDropdown(false);
        }
        catch (e) {
            console.error(e);
        }
    }
    
    if (isLoading) {
        return (<Spinner />);
    }
    else if (isSuccess && board) {
        return (
            <>
                <div className="board-navbar container">
                    <h2 className="nav-title">
                        {board.title}
                    </h2>
                    <button className="nav-btn">
                        Favorite
                        <IoStarOutline className="icon" />
                    </button>
                    <button className="nav-btn">
                        {board.isPrivate ? "Private" : "Public"}
                        {board.isPrivate ? <IoLockClosedOutline className="icon" /> : <IoLockOpenOutline className="icon" />}
                    </button>
                </div>
                <div className="columns-section">
                    {board.columns?.map((col) => <ColumnItem column={col} key={col.id} />)}
                    <div className="new-column-item">
                        <input
                            className="new-col-input"
                            placeholder="Add a new column"
                            value={column.title}
                            onFocus={() => togleDropdown(true)}
                            onChange={e => setColumn({ ...column, title: e.target.value })}
                        />
                        {isNewColumnDropdownOpen && (
                            <div className="save-section">
                                <button className="save-btn" onClick={() => onSubmitClicked(board.id)}>Save</button>
                                <IoClose className="icon-close" onClick={() => togleDropdown(false)} />
                            </div>
                        )}
                    </div>
                </div>
            </>
        );
    }
    else {
        return (<Navigate to="/error" replace />);
    }
}

export default BoardPage;