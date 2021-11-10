import * as React from 'react';
import {IMessageResponse} from "../../apiModels/messagesApi/Response/IMessageResponse";
import {Card, CardContent, Typography} from "@mui/material";
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
        messageReceived: {
            width: '440px',
            alignSelf: 'start'
        },
        messageSent: {
            width: '440px',
            alignSelf: 'end'
        }
    }),
);

export function ChatMessage(data: IMessageResponse) {
    const classes = useStyles();

    return (
        /*Check if username is current user to decide styling*/
    <Card className={data.username === null ? classes.messageSent : classes.messageReceived}>
        <CardContent>
            <Typography sx={{fontSize: 10}} color="text.secondary" gutterBottom>
                {data.username}
            </Typography>
            <Typography sx={{fontSize: 12}} variant="h5" component="div">
                {data.text}
            </Typography>
            <Typography sx={{fontSize: 8, mb: 1.5}} color="text.secondary">
                {data.created}
            </Typography>
        </CardContent>
    </Card>
)
    ;
}