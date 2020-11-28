import axios, {AxiosPromise} from "axios";
import {IApiResult} from "../apiModels/common/IApiResult";
import {ITodoTaskResponse} from "../apiModels/response/ITodoTaskResponse";

class TodoTasksApi {
    getAll(): AxiosPromise<IApiResult<ITodoTaskResponse[]>> {
        return axios({
            method: 'get',
            url: 'api/todoTasks/getAll',
        },)
    }
}

const api = new TodoTasksApi();

export default api as TodoTasksApi;
