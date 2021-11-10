import axios, {AxiosPromise} from "axios";
import {IApiResult} from "../apiModels/common/IApiResult";
import SessionService from "../services/SessionService";
import {IMessageResponse} from "../apiModels/messagesApi/Response/IMessageResponse";

class MessagesApi {
    //Chat api will get the chat with all messages
/*    getByChatId(chatId: string): AxiosPromise<IApiResult<IMessageResponse[]>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: `api/message/getByChatId/${chatId}`,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        })
    }*/

    getRecentMessages(): AxiosPromise<IApiResult<IMessageResponse[]>> {
        return axios({
            method: 'get',
            timeout: 2000,
            url: `api/messages/getRecentMessages`,
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        })
    }
}

const api = new MessagesApi();

export default api as MessagesApi;