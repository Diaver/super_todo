import axios, {AxiosPromise} from "axios";
import {IApiResult} from "../apiModels/common/IApiResult";
import SessionService from "../services/SessionService";
import {IContactResponse} from "../apiModels/contactsApi/Response/IContactResponse";

class ContactsApi {
    getAll(): AxiosPromise<IApiResult<IContactResponse[]>> {
        return axios( {
            method: 'get',
            timeout: 2000,
            url: 'api/contacts/getAll',
            headers: {Authorization: `Bearer ${SessionService.getSessionGuid()}`}
        })
    }    
}

const api = new ContactsApi();

export default api as ContactsApi;