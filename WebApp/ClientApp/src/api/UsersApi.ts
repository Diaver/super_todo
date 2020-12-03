import axios, {AxiosPromise} from "axios";
import {IApiResult, IApiResultBase} from "../apiModels/common/IApiResult";
import {IUserCreateRequest} from "../apiModels/usersApi/Request/IUserCreateRequest";
import {IUserUpdateRequest} from "../apiModels/usersApi/Request/IUserUpdateRequest";
import {IUserResponse} from "../apiModels/usersApi/Response/IUserResponse";
import {IUserIdRequest} from "../apiModels/usersApi/Request/IUserIdRequest";

class UsersApi {
    getAll(): AxiosPromise<IApiResult<IUserResponse[]>> {
        return axios({
            method: 'get',
            url: 'api/users/getAll',
        },)
    }
    
    add(userRequest: IUserCreateRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            url: 'api/users/add',
            data: userRequest
        },)
    }

    update(userResponse: IUserUpdateRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            url: 'api/users/update',
            data: userResponse
        },)
    }

    delete(userIdRequest: IUserIdRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            url: 'api/users/delete',
            data: userIdRequest
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
