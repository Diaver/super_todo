import * as React from 'react';
import {useDispatch, useSelector} from "react-redux";
import {List, ListSubheader} from "@mui/material";
import {chatSlice, loadRecentMessagesAsync, recentMessagesSelector, searchTextSelector} from "./chatSlice";
import {useEffect} from "react";
import {RecentMessage} from "./RecentMessage";

export function RecentMessages() {
    const recentMessages = useSelector(recentMessagesSelector);
    const searchText = useSelector(searchTextSelector);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(loadRecentMessagesAsync());
    }, []);

    useEffect(() => {
        dispatch(chatSlice.actions.setFilteredRecentMessages(searchText));
    }, [searchText]);

    return (
        <List>
            <ListSubheader sx={{bgcolor: 'background.paper'}} disableSticky={true}>
                Recent messages
            </ListSubheader>
            {recentMessages.map(({messageId, username, text}) => (
                <RecentMessage key={messageId} messageId={messageId} username={username} text={text} chatId="" created=""/>
            ))}
        </List>
    );
}