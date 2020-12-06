import axios, {AxiosPromise} from "axios";
import {IApiResult, IApiResultBase} from "../apiModels/common/IApiResult";
import {ITodoTaskResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskResponse";
import {ITodoTaskUserResponse} from "../apiModels/todoTasksApi/Response/ITodoTaskUserResponse";
import {ITodoTaskCreateRequest} from "../apiModels/todoTasksApi/Request/ITodoTaskCreateRequest";
import {ITodoTaskIdRequest} from "../apiModels/todoTasksApi/Request/ITodoTaskIdRequest";
import SessionService from "../services/SessionService";

class TodoTasksApi {
    getAll(): AxiosPromise<IApiResult<ITodoTaskResponse[]>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: 'api/todoTasks/getAll',
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }

    getAllUsers(): AxiosPromise<IApiResult<ITodoTaskUserResponse[]>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: 'api/todoTasks/getAllUsers',
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }

    getByUserId(userId: string): AxiosPromise<IApiResult<ITodoTaskResponse[]>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: `api/todoTasks/getByUserId/${userId}`,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }

    add(todoTaskCreateRequest: ITodoTaskCreateRequest): AxiosPromise<IApiResult<ITodoTaskResponse>> {
        return axios({
            method: 'put',
            timeout: 2000,
            url: `api/todoTasks/add/`,
            data: todoTaskCreateRequest,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }

    delete(todoTaskIdRequest: ITodoTaskIdRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            timeout: 2000,
            url: `api/todoTasks/delete/`,
            data: todoTaskIdRequest,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }

    complete(todoTaskIdRequest: ITodoTaskIdRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            timeout: 2000,
            url: `api/todoTasks/complete/`,
            data: todoTaskIdRequest,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }
}

const api = new TodoTasksApi();

export default api as TodoTasksApi;
