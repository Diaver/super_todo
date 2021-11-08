import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from '../../app/rootStore';
import NotificationService from "../../services/NotificationService";
import ContactsApi from "../../api/ContactsApi";
import MessagesApi from "../../api/MessagesApi";
import {IMessageResponse} from "../../apiModels/messagesApi/IMessageResponse";
import {IContactResponse} from "../../apiModels/contactsApi/Response/IContactResponse";

interface MessengerState {
    messages: IMessageResponse[];
    contacts: IContactResponse[];
    recentMessages?: IMessageResponse[];
    selectedContact?: IContactResponse;
    chat?: any;
    loading: boolean;
    messageText: string;
    searchText: string;
}

const initialState: MessengerState = {
    messages: [],
    contacts: [],
    loading: false,
    messageText: "",
    searchText: ""
};

export const messengerSlice = createSlice({
    name: 'messenger',
    initialState,
    reducers: {
        messagesLoaded: (state, action: PayloadAction<IMessageResponse[]>) => {
            state.messages = action.payload
        },
        contactsLoaded: (state, action: PayloadAction<IContactResponse[]>) => {
            state.contacts = action.payload
        },
        recentMessagesLoaded: (state, action: PayloadAction<IMessageResponse[]>) => {
            state.recentMessages = action.payload
        },
        setSelectedContact: (state, action: PayloadAction<IContactResponse>) => {
            state.selectedContact = action.payload
        },
        setChat: (state, action: PayloadAction<any>) => {
            state.chat = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
        setSearchText: (state, action: PayloadAction<string>) => {
            state.searchText = action.payload
        },
        setMessageText: (state, action: PayloadAction<string>) => {
            state.searchText = action.payload
        },
        addMessage: (state, action: PayloadAction<IMessageResponse>) => {
            state.messages = [...state.messages, action.payload];
        },
        finish: (state) => {
            state.searchText = "";
        },
    },
});

export const loadContactsAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(messengerSlice.actions.setLoading(true));

    try {
        let serverRequest = await ContactsApi.getAll();

        if (serverRequest.data.isSuccess) {
            dispatch(messengerSlice.actions.contactsLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    dispatch(messengerSlice.actions.setLoading(false));
}

/*export const loadMessagesByChatIdAsync = (): AppThunk => async (dispatch, getState) => {

    dispatch(messengerSlice.actions.setLoading(true));

    try {
        //getting first recent message to load initial chat
        const chatId = getState().messenger?.recentMessages?.find(x => x)?.chatId as string;

        if (!chatId) {
            return;
        }
        let serverRequest = await MessagesApi.getByChatId(chatId);

        if (serverRequest.data.isSuccess) {
            dispatch(messengerSlice.actions.messagesLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    dispatch(messengerSlice.actions.setLoading(false));
}*/

export const loadRecentMessages = (): AppThunk => async (dispatch, getState) => {

    dispatch(messengerSlice.actions.setLoading(true));

    try {
        let serverRequest = await MessagesApi.getRecentMessages();

        if (serverRequest.data.isSuccess) {
            dispatch(messengerSlice.actions.recentMessagesLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }
    dispatch(messengerSlice.actions.setLoading(false));
}

/*export const addAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(messengerSlice.actions.setLoading(true));
    try {
        const chatId = getState().messenger.chat?.chatId as string;
        const text = getState().messenger.messageText;

        const serverRequest = await MessagesApi.add({
            chatId,
            contactId,
            text
        });

        if (serverRequest.data.isSuccess) {
            dispatch(messengerSlice.actions.addMessage(serverRequest.data.data));
            NotificationService.onSuccessMessage("Task added");
        } else {
            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }

    dispatch(messengerSlice.actions.setLoading(false));
    dispatch(messengerSlice.actions.finish());
};*/

export const messagesSelector = (state: RootState) => state.messenger.messages;
export const contactsSelector = (state: RootState) => state.messenger.contacts;
export const searchTextSelector = (state: RootState) => state.messenger.searchText;
export const chatSelector = (state: RootState) => state.messenger.chat;

export default messengerSlice.reducer;
