export interface User {
  id: number;
  name: string;
  surname: string;
  birthDate: Date;
  userTypeId: number;
  userType: string;
  userTitleId: number;
  userTitle: string;
  emailAddress: string;
  isActive: boolean;
}