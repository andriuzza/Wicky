import EmployeeWorkPhoto from "./EmployeeWorkPhoto";
import EmployeeExperiance from "./EmployeeExperiance";
import EmployeeRating from "./EmployeeRating";

export default class Employee {
    id: string;
    name: string;
    lastName: string;
    address: string;
    photos: EmployeeWorkPhoto[];
    experiances: EmployeeExperiance[];
    rating: EmployeeRating[];   
}



