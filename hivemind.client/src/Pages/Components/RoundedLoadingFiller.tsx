import { TableRow, TableCell } from '@mui/material';
import Skeleton from '@mui/material/Skeleton';

interface RoundedLoadingFillerProps {
    size: number;
    width: number;
}

const RoundedLoadingFiller = ({ size, width }: RoundedLoadingFillerProps) => {
    return [...new Array(size)].map((_, i) => (
        <TableRow key={i}>
            {
                [...Array(width)].map((_, index) => ( 
                    <TableCell component="th" scope="row" key={index}>
                        <Skeleton animation="wave" variant="text" />
                    </TableCell>
                ))
            }
        </TableRow>
    ))
};

export default RoundedLoadingFiller;