import axios, {AxiosPromise} from "axios";
import {IApiResult} from "../apiModels/common/IApiResult";
import {ITodoTaskResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskResponse";
import {ITodoTaskUserResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskUserResponse";
import {ITodoTaskCreateRequest} from "../apiModels/todoTasksApi/Request/ITodoTaskCreateRequest";

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

    getByUserId(userId: string): AxiosPromise<IApiResult<ITodoTaskResponse[]>> {
        return axios({
            method: 'get',
            url: `api/todoTasks/getByUserId/${userId}`,
        },)
    }

    add(todoTaskCreateRequest: ITodoTaskCreateRequest): AxiosPromise<IApiResult<ITodoTaskResponse>> {
        return axios({
            method: 'put',
            url: `api/todoTasks/add/`,
            data: todoTaskCreateRequest
        },)
    }

}

const api = new TodoTasksApi();

export default api as TodoTasksApi;
