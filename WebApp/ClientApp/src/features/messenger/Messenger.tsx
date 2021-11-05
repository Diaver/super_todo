import * as React from 'react';
import {
    Avatar, Button, Card, CardContent,
    Container,
    Grid,
    InputAdornment,
    List,
    ListItem, ListItemAvatar, ListItemText,
    ListSubheader,
    Paper,
    Stack,
    TextField,
    Typography
} from '@mui/material';
import {createStyles, makeStyles, Theme} from "@material-ui/core/styles";
import {Search, Send} from "@material-ui/icons";
import {useEffect, useRef, useState} from "react";

let allContacts = [
    {
        id: 1,
        name: 'Steve Jobs',
        image: '/static/images/avatar/5.jpg'
    },
    {
        id: 2,
        name: 'Kanye West',
        image: '/static/images/avatar/1.jpg'
    },
    {
        id: 3,
        name: 'Eric Cartman',
        image: ''
    },
    {
        id: 4,
        name: 'Rick Sanchez',
        image: ''
    },
    {
        id: 5,
        name: 'Morty Smith',
        image: ''
    },
    {
        id: 6,
        name: 'Rendell Locke',
        image: ''
    },
    {
        id: 7,
        name: 'Kinsey Locke',
        image: ''
    }
];

allContacts = allContacts.sort(function(a, b) {
    var nameA = a.name.toUpperCase(); // ignore upper and lowercase
    var nameB = b.name.toUpperCase(); // ignore upper and lowercase
    if (nameA < nameB) {
        return -1;
    }
    if (nameA > nameB) {
        return 1;
    }

    // names must be equal
    return 0;
});

let allRecentChats = [
    {
        id: 1,
        name: 'Steve Jobs',
        message: "I'll be in the neighbourhood this week. Let's grab a bite to eat",
        image: '/static/images/avatar/5.jpg'
    },
    {
        id: 2,
        name: 'Kanye West',
        message: `Do you have a suggestion for a good present for John on his work
      anniversary. I am really confused & would love your thoughts on it.`,
        image: '/static/images/avatar/1.jpg'
    }
]

let currentMessages = [
    {
        id: 1,
        message: 'Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
        dateTimeSent: new Date(),
        sentBy: 'kanye west'
    },
    {
        id: 2,
        message: '2 Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
        dateTimeSent: new Date(),
        sentBy: 'kanye west'
    },
    {
        id: 3,
        message: '3 Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
        dateTimeSent: new Date(),
        sentBy: null
    },
    {
        id: 4,
        message: '4 Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n',
        dateTimeSent: new Date(),
        sentBy: 'kanye west'
    }
]

let chat = {
    id: 1,
    name: 'My test chatroom',
    messages: currentMessages
};

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

export function Messenger() {
    // const text = useSelector(textSelector);
    // const allContacts = useSelector(contactsSelector);
    // const recentMessages = useSelector(messagesSelector);
    // const dispatch = useDispatch();
    const classes = useStyles();
    const [contacts, setContacts] = useState(allContacts);
    const [recentChats, setRecentChats] = useState(allRecentChats);
    const messagesEndRef = useRef(null);
    
    const scrollToBottom = () => {
        if (!!messagesEndRef.current) {
            // @ts-ignore
            messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
        }
    };
    
    useEffect(scrollToBottom, [currentMessages]);
    
    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        // dispatch(tasksSlice.actions.setText(event.target.value));
        
        const searchedContacts = allContacts.filter( function(x) {
            return x.name.toUpperCase().includes(event.target.value.toUpperCase());
        });
        const searchedRecentChats = allRecentChats.filter( function(x) {
            return x.name.toUpperCase().includes(event.target.value.toUpperCase());
        }); 
        
        setContacts(searchedContacts);
        setRecentChats(searchedRecentChats);
    }
    
    return (
        <Container component="main" maxWidth={false}>
            <Grid container spacing={2}> 
                <Grid item xs={2}>
                    <TextField id="standard-basic" label="Search for contacts" variant="standard"
                               // value={text}
                               fullWidth
                               onChange={handleChange} InputProps={{
                        endAdornment: (
                            <InputAdornment position="end">
                                <Search/>
                            </InputAdornment>
                        ),
                    }}/>
                </Grid>
                <Grid item xs={10}>
                    <Typography variant={"h4"}>
                        {chat.name}
                    </Typography>
                </Grid>
                <Grid item xs={2} className={classes.contacts}>
                    <Paper square sx={{ pb: '50px' }}>
                        <ListSubheader sx={{ bgcolor: 'background.paper' }} disableSticky={true}>
                            Recent messages
                        </ListSubheader>
                        <List>
                            {recentChats.map(({ id, name, message, image }) => (
                                <React.Fragment key={id}>
                                    <ListItem button>
                                        <ListItemAvatar>
                                            <Avatar alt="Profile Picture" src={image} />
                                        </ListItemAvatar>
                                        <ListItemText primary={name} secondary={message} />
                                    </ListItem>
                                </React.Fragment>
                            ))}
                        </List>
                        <List >
                            <ListSubheader sx={{ bgcolor: 'background.paper' }} disableSticky={true}>
                                All contacts
                            </ListSubheader>
                            {contacts.map(({ id, name, image }) => (
                                <React.Fragment key={id}>
                                    <ListItem button>
                                        <ListItemAvatar>
                                            <Avatar alt="Profile Picture" src={image} />
                                        </ListItemAvatar>
                                        <ListItemText primary={name}/>
                                    </ListItem>
                                </React.Fragment>
                            ))}
                        </List>
                    </Paper>
                </Grid>
                <Grid item xs={10}>
                    <Grid container spacing={2} alignItems="center">
                        <Grid item xs={12}  className={classes.messages}>
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
                        </Grid>
                        <Grid item xs={10}>
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
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </Container>
    );
}