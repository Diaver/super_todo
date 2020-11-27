import React from 'react';
import {makeStyles} from "@material-ui/core/styles";
import {Button, Container, Link, Typography} from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexDirection: 'column',
        minHeight: 'calc(100vh - 200px)',
    },
    main: {
        marginTop: theme.spacing(8),
        marginBottom: theme.spacing(2),
    },
}));

export function PageNotFound() {
    const classes = useStyles();
    return (
        <div className={classes.root}>
            <Container component="main" className={classes.main} maxWidth="sm">
                <Typography variant="h3" component="h1" gutterBottom>
                    Sorry, page not found.
                </Typography>
                <Button component={Link} href="/">
                    Go to Home page
                </Button>
            </Container>
        </div>
    );
}