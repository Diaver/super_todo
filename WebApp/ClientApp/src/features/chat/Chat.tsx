import * as React from 'react';
import {
    Container,
    Grid, Paper
} from "@mui/material";
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";
import {AllContacts} from "./AllContacts";
import {RecentMessages} from "./RecentMessages";
import {SearchBar} from "./SearchBar";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
        contacts: {
            height: 'calc(100vh - 260px)',
            overflow: 'auto'
        },
        messages: {
            height: 'calc(100vh - 400px)',
            overflow: 'auto'
        },
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

export function Chat() {
    const classes = useStyles();

    return (
        <Container component="main" maxWidth={false}>
            <Grid container spacing={2}>
                <Grid item xs={2}>
                    <SearchBar/>
                </Grid>
                <Grid item xs={10}>
                    {/*<Typography variant={"h4"}>
                        {chat.name}
                    </Typography>*/}
                </Grid>
                <Grid item xs={2} className={classes.contacts}>
                    <Paper square sx={{pb: '50px'}}>
                        <RecentMessages/>
                        <AllContacts/>
                    </Paper>
                </Grid>
                <Grid item xs={10}>
                    <Grid container spacing={2} alignItems="center">
                        {/*                        <Grid item xs={12}  className={classes.messages}>
                            <Stack
                                direction="column"
                                justifyContent="flex-start"
                                alignItems="flex-start"
                                spacing={2}
                            >
                                { chat.messages.map(({id, message, dateTimeSent, sentBy}) => (
                                    <React.Fragment key={id}>
                                        <Card className={sentBy === null ? classes.messageSent : classes.messageReceived}>
                                            <CardContent>
                                                <Typography sx={{ fontSize: 10 }} color="text.secondary" gutterBottom>
                                                    {sentBy}
                                                </Typography>
                                                <Typography sx={{ fontSize: 12 }} variant="h5" component="div">
                                                    {message}
                                                </Typography>
                                                <Typography sx={{ fontSize: 8, mb: 1.5 }} color="text.secondary">
                                                    {dateTimeSent.toString()}
                                                </Typography>
                                            </CardContent>
                                        </Card>
                                    </React.Fragment>
                                ))}
                                <div ref={messagesEndRef} />
                            </Stack>
                        </Grid>*/}
                        {/*                        <Grid item xs={10}>
                            <TextField
                                id="outlined-multiline-static"
                                placeholder="Enter message"
                                multiline
                                rows={4}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <Button variant="contained" endIcon={<Send />} size="large"
                                    onClick={() => {console.log("message sent")}}>
                                Send
                            </Button>
                        </Grid>*/}
                    </Grid>
                </Grid>
            </Grid>
        </Container>
    );
}