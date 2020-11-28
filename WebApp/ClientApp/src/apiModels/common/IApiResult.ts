﻿import {ErrorMessagesEnum} from "./ErrorMessagesEnum";

export interface IApiResultBase {
    isSuccess: boolean;
    message: string;
    errorMessagesEnum: ErrorMessagesEnum;
}

export interface IApiResult<T> extends IApiResultBase {
    data: T;
}
