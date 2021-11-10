import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from "../../app/rootStore";
import {IContactResponse} from "../../apiModels/contactsApi/Response/IContactResponse";
import {IMessageResponse} from "../../apiModels/messagesApi/Response/IMessageResponse";
import {IChatResponse} from "../../apiModels/chatsApi/IChatResponse";
import NotificationService from "../../services/NotificationService";
import MessagesApi from "../../api/MessagesApi";

interface ChatState {
    searchText: string;
    messageText: string;
    contacts: IContactResponse[];
    filteredContacts: IContactResponse[];
    recentMessages: IMessageResponse[];
    filteredRecentMessages: IMessageResponse[];
    chat: IChatResponse;
    chatId: string;
    loading: boolean;
}

const initialState: ChatState = {
    searchText: "",
    messageText: "",
    contacts: [],
    filteredContacts: [],
    recentMessages: [],
    filteredRecentMessages: [],
    chat: {
        chatId: "",
        name: "",
        messages: []
    },
    chatId: "",
    loading: false
}

export const chatSlice = createSlice( {
    name: 'chat',
    initialState,
    reducers: {
        contactsLoaded: (state, action: PayloadAction<any>) => { // to-do: update PayloadAction type to IContactResponse[]
            state.contacts = action.payload;
            state.filteredContacts = action.payload;
        },
        recentMessagesLoaded: (state, action: PayloadAction<any>) => { // to-do: update PayloadAction type to IMessageResponse[]
            state.recentMessages = action.payload;
            state.filteredRecentMessages = action.payload;
        },
        chatLoaded: (state, action: PayloadAction<any>) => { // to-do: update PayloadAction type to IChatResponse
          state.chat = action.payload;  
        },
        setSearchText: (state, action: PayloadAction<string>) => {
            state.searchText = action.payload
        },
        setMessageText: (state, action: PayloadAction<string>) => {
            state.messageText = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
        addMessage: (state, action: PayloadAction<any>) => { //to-do: update PayloadAction type to IMessageResponse
            state.chat.messages = [...state.chat.messages, action.payload]
        },       
        //is there a better way to filter this data?
        setFilteredContacts: (state, action: PayloadAction<string>) => {
            state.filteredContacts = state.contacts.filter( function(x) {
                return x.name.toUpperCase().includes(action.payload.toUpperCase());
            });
        },
        setFilteredRecentMessages: (state, action: PayloadAction<string>) => {
            state.filteredRecentMessages = state.recentMessages.filter( function(x) {
                return x.username.toUpperCase().includes(action.payload.toUpperCase());
            });
        },
        finish: (state) => {
            state.searchText = "";
        }
    }
});

export const loadContactsAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(chatSlice.actions.setLoading(true));
    
/*    try {
        let serverRequest = await ContactsApi.getAll();

        if (serverRequest.data.isSuccess) {
            dispatch(chatSlice.actions.contactsLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }*/

    let allContacts = [
        {
            contactId: 1,
            name: 'Steve Jobs'
        },
        {
            contactId: 2,
            name: 'Kanye West'
        },
        {
            contactId: 3,
            name: 'Eric Cartman'
        },
        {
            contactId: 4,
            name: 'Rick Sanchez'
        },
        {
            contactId: 5,
            name: 'Morty Smith'
        },
        {
            contactId: 6,
            name: 'Rendell Locke'
        },
        {
            contactId: 7,
            name: 'Kinsey Locke'
        }
    ];
    
    dispatch(chatSlice.actions.contactsLoaded(allContacts));

    dispatch(chatSlice.actions.setLoading(false));
}

export const loadRecentMessagesAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(chatSlice.actions.setLoading(true));

/*        try {
            let serverRequest = await MessagesApi.getRecentMessages();
    
            if (serverRequest.data.isSuccess) {
                dispatch(chatSlice.actions.recentMessagesLoaded(serverRequest.data.data));
            } else {
                NotificationService.onRequestFailed(serverRequest.data)
            }
        } catch (e) {
            NotificationService.onPromiseRejected(e);
        }*/

    let recentMessages = [
        {
            messageId: 1,
            username: 'Steve Jobs',
            text: "I'll be in the neighbourhood this week. Let's grab a bite to eat",
            created: new Date().toString()
        },
        {
            messageId: 2,
            username: 'Kanye West',
            text: `Do you have a suggestion for a good present for John on his work
      anniversary. I am really confused & would love your thoughts on it.`,
            created: new Date().toString()
        }
    ]

    dispatch(chatSlice.actions.recentMessagesLoaded(recentMessages));

    dispatch(chatSlice.actions.setLoading(false));
}

export const loadChatAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(chatSlice.actions.setLoading(true));

    /*try {
        const chatId = getState().chat.chatId as string;
        let serverRequest = await ChatsApi.get(chatId);

        if (serverRequest.data.isSuccess) {
            dispatch(chatSlice.actions.recentMessagesLoaded(serverRequest.data.data));
        } else {
            NotificationService.onRequestFailed(serverRequest.data)
        }
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }*/

    let currentMessages = [
        {
            messageId: 1,
            text: 'Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
            created: new Date().toString(),
            username: 'kanye west'
        },
        {
            messageId: 2,
            text: '2 Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
            created: new Date().toString(),
            username: 'kanye west'
        },
        {
            messageId: 3,
            text: '3 Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
            created: new Date().toString(),
            username: null
        },
        {
            messageId: 4,
            text: '4 Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
            created: new Date().toString(),
            username: 'kanye west'
        }
    ]

    let chat = {
        chatId: 1,
        name: 'My test chatroom',
        messages: currentMessages
    };

    dispatch(chatSlice.actions.chatLoaded(chat));
    
    dispatch(chatSlice.actions.setLoading(false));
}

export const addMessageAsync = (): AppThunk => async (dispatch, getState) => {
    dispatch(chatSlice.actions.setLoading(true));

    /*try {
        const chatId = getState().chat.chatId as string;
        const messageText = getState().chat.messageText as string;
        const currentUser = getState().chat.currentUser as string;

        let serverRequest = await MessagesApi.add({
            chatId,
            messageText,
            currentUser
        })

        if (serverRequest.data.isSuccess) {
            dispatch(chatSlice.actions.addMessage(serverRequest.data.data));
            NotificationService.onSuccessMessage("Message sent");
        } else {
            NotificationService.onRequestFailed(serverRequest.data, 5000)
        }        
    } catch (e) {
        NotificationService.onPromiseRejected(e);
    }*/

    dispatch(chatSlice.actions.setLoading(false));
}

export const searchTextSelector = (state: RootState) => state.chat.searchText;
export const contactsSelector = (state: RootState) => state.chat.filteredContacts;
export const recentMessagesSelector = (state: RootState) => state.chat.filteredRecentMessages;
export const chatSelector = (state: RootState) => state.chat.chat;

export default chatSlice.reducer;