import axios, {AxiosPromise} from "axios";
import {IApiResult, IApiResultBase} from "../apiModels/common/IApiResult";
import {IUserResponse} from "../apiModels/response/IUserResponse";
import {IUserRequest} from "../apiModels/request/IUserRequest";

class UsersApi {
    getAll(): AxiosPromise<IApiResult<IUserResponse[]>> {
        return axios({
            method: 'get',
            url: 'api/users/getAll',
        },)
    }
    add(userRequest: IUserRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            url: 'api/users/add',
            data: userRequest
        },)
    }
}

const api = new UsersApi();

export default api as UsersApi;
