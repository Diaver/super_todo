import * as React from 'react';
import {Grid, Stack, Typography} from "@mui/material";
import {ChatMessage} from "./ChatMessage";
import {useEffect, useRef} from "react";
import {chatSelector, loadChatAsync} from "./chatSlice";
import {useDispatch, useSelector} from "react-redux";
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
        messages: {
            height: 'calc(100vh - 440px)',
            overflow: 'auto'
        },
        chatName: {
            paddingLeft: "16px"
        }
    }),
);

export function ChatMessages() {
    const classes = useStyles();
    const chat = useSelector(chatSelector);
    const dispatch = useDispatch();
    
    const messagesEndRef = useRef(null);
    const scrollToBottom = () => {
        if (!!messagesEndRef.current) {
            // @ts-ignore
            messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
        }
    };
    
    useEffect(() => {
        dispatch(loadChatAsync());
    },[]);

    useEffect(scrollToBottom, [chat.messages]);

    return (
        <Grid container spacing={2} alignItems="center">
            <Typography variant={"h4"} className={classes.chatName}>
                {chat.name}
            </Typography>
            <Grid item xs={12}  className={classes.messages}>
                <Stack
                    direction="column"
                    spacing={2}
                >
                    { chat.messages.map(({messageId, chatId, username, text, created}) => (
                        <ChatMessage key={messageId} messageId={messageId} chatId={chatId} username={username} text={text} created={created}/>
                    ))}
                    <div ref={messagesEndRef} />
                </Stack>
            </Grid>
        </Grid>
    );
}