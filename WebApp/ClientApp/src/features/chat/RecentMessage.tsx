import * as React from 'react';
import {Avatar, ListItem, ListItemAvatar, ListItemText} from "@mui/material";
import {IMessageResponse} from "../../apiModels/messagesApi/Response/IMessageResponse";

export function RecentMessage(data: IMessageResponse) {

    return (
        <ListItem button>
            <ListItemAvatar>
                <Avatar alt="Profile Picture"/>
            </ListItemAvatar>
            <ListItemText primary={data.username} secondary={data.text}/>
        </ListItem>
    );
}