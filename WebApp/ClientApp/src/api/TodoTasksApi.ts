import axios, {AxiosPromise} from "axios";
import {IApiResult} from "../apiModels/common/IApiResult";
import {ITodoTaskResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskResponse";
import {ITodoTaskUserResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskUserResponse";

class TodoTasksApi {
    getAll(): AxiosPromise<IApiResult<ITodoTaskResponse[]>> {
        return axios({
            method: 'get',
            url: 'api/todoTasks/getAll',
        },)
    }

    getAllUsers(): AxiosPromise<IApiResult<ITodoTaskUserResponse[]>> {
        return axios({
            method: 'get',
            url: 'api/todoTasks/getAllUsers',
        },)
    }

}

const api = new TodoTasksApi();

export default api as TodoTasksApi;
