import {IMessageResponse} from "../messagesApi/Response/IMessageResponse";

export interface IChatResponse {
    chatId: string;
    name: string;
    messages: IMessageResponse[];
}