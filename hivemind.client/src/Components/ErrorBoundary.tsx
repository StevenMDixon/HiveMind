import { Component, ErrorInfo, ReactNode } from 'react';
import { Alert, Button, Box } from '@mui/material';

interface Props {
    children: ReactNode;
    fallback?: (error: Error, reset: () => void) => ReactNode;
}

interface State {
    hasError: boolean;
    error: Error | null;
}

class ErrorBoundary extends Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = { hasError: false, error: null };
    }

    static getDerivedStateFromError(error: Error): State {
        return { hasError: true, error };
    }

    componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error('Error caught by boundary:', error, errorInfo);
    }

    resetError = () => {
        this.setState({ hasError: false, error: null });
    };

    render() {
        if (this.state.hasError && this.state.error) {
            if (this.props.fallback) {
                return this.props.fallback(this.state.error, this.resetError);
            }

            return (
                <Box sx={{ p: 2 }}>
                    <Alert 
                        severity="error" 
                        action={
                            <Button color="inherit" size="small" onClick={this.resetError}>
                                Retry
                            </Button>
                        }
                    >
                        <strong>Error:</strong> {this.state.error.message}
                    </Alert>
                </Box>
            );
        }

        return this.props.children;
    }
}

export default ErrorBoundary;