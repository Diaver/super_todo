import React, {PropsWithChildren} from "react";
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Link from "@material-ui/core/Link";
import Box from "@material-ui/core/Box";

type TProps = {
    isDesktopScreen: boolean;
};

const useStyles = makeStyles((theme: Theme) =>
    createStyles({}),
);

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

interface MainLayoutProps {
    showSearchBar?: boolean
    showFooter?: boolean
}

export function MainLayout(props: PropsWithChildren<MainLayoutProps>) {
    return (
        <div>
            <main>
                {props.children}
                {
                    props.showFooter &&
                    <Box mt={8}>
                        <Copyright/>
                    </Box>
                }
            </main>
        </div>
    );
}