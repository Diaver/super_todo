import axios, {AxiosPromise} from "axios";
import {IApiResult, IApiResultBase} from "../apiModels/common/IApiResult";
import {ITodoTaskResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskResponse";
import {ITodoTaskUserResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskUserResponse";
import {ITodoTaskCreateRequest} from "../apiModels/todoTasksApi/Request/ITodoTaskCreateRequest";
import {ITodoTaskIdRequest} from "../apiModels/todoTasksApi/Request/ITodoTaskIdRequest";

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

    delete(todoTaskIdRequest: ITodoTaskIdRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            url: `api/todoTasks/delete/`,
            data: todoTaskIdRequest
        },)
    }

    complete(todoTaskIdRequest: ITodoTaskIdRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            url: `api/todoTasks/complete/`,
            data: todoTaskIdRequest
        },)
    }
}

const api = new TodoTasksApi();

export default api as TodoTasksApi;
