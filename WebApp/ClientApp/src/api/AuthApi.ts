import axios, {AxiosPromise} from "axios";
import {IApiResult} from "../apiModels/common/IApiResult";
import {ILoginRequest} from "../apiModels/auth/request/ILoginRequest";
import {ILoginResponse} from "../apiModels/auth/response/ILoginResponse";

class AuthApi {
    login(userIdRequest: ILoginRequest): AxiosPromise<IApiResult<ILoginResponse>> {
        return axios({
            method: 'post',
            timeout: 2000,
            url: 'api/auth/login',
            data: userIdRequest
        },)
    }
}

const api = new AuthApi();

export default api as AuthApi;
