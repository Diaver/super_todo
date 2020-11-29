import React from 'react';
import Typography from '@material-ui/core/Typography';
import {Link} from "@material-ui/core";

export function Copyright() {
    return (
        <Typography variant="body2" color="textSecondary" align="center">
            {'© '}
            <Link color="inherit" href="/">
                Super ToDo
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}
