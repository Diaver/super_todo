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

    update(userResponse: IUserResponse): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            url: 'api/users/update',
            data: userResponse
        },)
    }
    
    getById(userId: string): AxiosPromise<IApiResult<IUserResponse>> {
        return axios({
            method: 'get',
            url: `api/users/getById/${userId}`,
        },)
    }
}

const api = new UsersApi();

export default api as UsersApi;
