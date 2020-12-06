import axios, {AxiosPromise} from "axios";
import {IApiResult} from "../apiModels/common/IApiResult";
import {ILoginRequest} from "../apiModels/auth/request/ILoginRequest";
import {ILoginResponse} from "../apiModels/auth/response/ILoginResponse";
import SessionService from "../services/SessionService";
import {ICurrentUserResponse} from "../apiModels/auth/response/ICurrentUserResponse";

class AuthApi {
    login(userIdRequest: ILoginRequest): AxiosPromise<IApiResult<ILoginResponse>> {
        return axios({
            method: 'post',
            timeout: 2000,
            url: 'api/auth/login',
            data: userIdRequest
        })
    }

    getCurrentUser(): AxiosPromise<IApiResult<ICurrentUserResponse>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: 'api/auth/getCurrentUser',
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        })
    }
}

const api = new AuthApi();

export default api as AuthApi;
