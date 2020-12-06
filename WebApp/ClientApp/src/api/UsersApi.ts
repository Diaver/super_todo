import axios, {AxiosPromise} from "axios";
import {IApiResult, IApiResultBase} from "../apiModels/common/IApiResult";
import {IUserCreateRequest} from "../apiModels/usersApi/Request/IUserCreateRequest";
import {IUserUpdateRequest} from "../apiModels/usersApi/Request/IUserUpdateRequest";
import {IUserResponse} from "../apiModels/usersApi/Response/IUserResponse";
import {IUserIdRequest} from "../apiModels/usersApi/Request/IUserIdRequest";
import SessionService from "../services/SessionService";

class UsersApi {
    getAll(): AxiosPromise<IApiResult<IUserResponse[]>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: 'api/users/getAll',
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }
    
    add(userRequest: IUserCreateRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            timeout: 2000,
            url: 'api/users/add',
            data: userRequest,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
            
        },)
    }

    update(userResponse: IUserUpdateRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            timeout: 2000,
            url: 'api/users/update',
            data: userResponse,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }

    delete(userIdRequest: IUserIdRequest): AxiosPromise<IApiResultBase> {
        return axios({
            method: 'put',
            timeout: 2000,
            url: 'api/users/delete',
            data: userIdRequest,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }
    
    getById(userId: string): AxiosPromise<IApiResult<IUserResponse>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: `api/users/getById/${userId}`,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        },)
    }
}

const api = new UsersApi();

export default api as UsersApi;
