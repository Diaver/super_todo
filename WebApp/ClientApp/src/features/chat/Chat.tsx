import * as React from 'react';
import {
    Container,
    Grid, Paper
} from "@mui/material";
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";
import {Contacts} from "./Contacts";
import {RecentMessages} from "./RecentMessages";
import {SearchBar} from "./SearchBar";
import {SendMessage} from "./SendMessage";
import {ChatMessages} from "./ChatMessages";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
        contacts: {
            height: 'calc(100vh - 260px)',
            overflow: 'auto'
        }
    }),
);

export function Chat() {
    const classes = useStyles();

    return (
        <Container component="main" maxWidth={false}>
            <Grid container spacing={2}>
                <Grid item xs={2}>
                    <SearchBar/>
                </Grid>
                <Grid item xs={10}/>
                <Grid item xs={2} className={classes.contacts}>
                    <Paper square sx={{pb: '50px'}}>
                        <RecentMessages/>
                        <Contacts/>
                    </Paper>
                </Grid>
                <Grid item xs={10}>
                    <ChatMessages/>
                    <br/>
                    <SendMessage/>
                </Grid>
            </Grid>
        </Container>
    );
}