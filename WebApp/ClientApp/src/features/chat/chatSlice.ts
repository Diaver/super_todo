import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {AppThunk, RootState} from "../../app/rootStore";
import {IContactResponse} from "../../apiModels/contactsApi/Response/IContactResponse";
import {IMessageResponse} from "../../apiModels/messagesApi/IMessageResponse";

interface ChatState {
    searchText: string;
    contacts: IContactResponse[];
    filteredContacts: IContactResponse[];
    recentMessages: IMessageResponse[];
    filteredRecentMessages: IMessageResponse[];
    loading: boolean;
}

const initialState: ChatState = {
    searchText: "",
    contacts: [],
    filteredContacts: [],
    recentMessages: [],
    filteredRecentMessages: [],
    loading: false
}

export const chatSlice = createSlice( {
    name: 'chat',
    initialState,
    reducers: {
        contactsLoaded: (state, action: PayloadAction<any>) => { // to-do: upload PayloadAction type to IContactResponse[]
            state.contacts = action.payload;
            state.filteredContacts = action.payload;
        },
        recentMessagesLoaded: (state, action: PayloadAction<any>) => { // to-do: upload PayloadAction type to IMessageResponse[]
            state.recentMessages = action.payload;
            state.filteredRecentMessages = action.payload;
        },
        setSearchText: (state, action: PayloadAction<string>) => {
            state.searchText = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
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

export const searchTextSelector = (state: RootState) => state.chat.searchText;
export const contactsSelector = (state: RootState) => state.chat.filteredContacts;
export const recentMessagesSelector = (state: RootState) => state.chat.filteredRecentMessages;

export default chatSlice.reducer;