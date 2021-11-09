import * as React from 'react';
import {useDispatch, useSelector} from "react-redux";
import {Avatar, List, ListItem, ListItemAvatar, ListItemText, ListSubheader} from "@mui/material";
import {chatSlice, loadRecentMessagesAsync, recentMessagesSelector, searchTextSelector} from "./chatSlice";
import {useEffect} from "react";

export function RecentMessages() {
    const recentMessages = useSelector(recentMessagesSelector);
    const searchText = useSelector(searchTextSelector);
    const dispatch = useDispatch();
    
    useEffect(() => {
        dispatch(loadRecentMessagesAsync());
    }, [])
    
    useEffect(() => {
        dispatch(chatSlice.actions.setFilteredRecentMessages(searchText));
    }, [searchText])

    return (
    <List>
        <ListSubheader sx={{bgcolor: 'background.paper'}} disableSticky={true}>
            Recent messages
        </ListSubheader>
        {recentMessages.map(({messageId, username, text}) => (
            <React.Fragment key={messageId}>
                <ListItem button>
                    <ListItemAvatar>
                        <Avatar alt="Profile Picture"/>
                    </ListItemAvatar>
                    <ListItemText primary={username} secondary={text}/>
                </ListItem>
            </React.Fragment>
        ))}
    </List>
);
}