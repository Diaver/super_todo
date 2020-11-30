import {TodoTaskStatus} from "../../common/TodoTaskStatus";

export interface ITodoTaskResponse {
    todoTaskId: string;
    userId: string;
    text: string;
    status: TodoTaskStatus;
}