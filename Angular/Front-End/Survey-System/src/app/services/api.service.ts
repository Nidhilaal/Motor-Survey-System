import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserMasterEntity } from '../shared/models/master/UserMasterEntity';
import { catchError, Observable, throwError } from 'rxjs';
import { CodesMasterEntity } from '../shared/models/master/CodesMasterEntity';
import { ErrorMasterEntity } from '../shared/models/master/ErrorMasterEnitity';
import { MotorClmSurHdrEntity } from '../shared/models/transaction/MotorClmSurHdrEntity';
import { MotorClaimEntity } from '../shared/models/transaction/MotorClaimEntity';
import { MotorClmSurDtlEntity } from '../shared/models/transaction/MotorClmSurDtlEntity';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'https://localhost:7156/api'; 
 
  constructor(private http: HttpClient) { }
 

  Validatelogin(user: UserMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/LoginAPI/Validatelogin`, user, { responseType: 'text' });
  }

  GetUserType(user: UserMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/LoginAPI/GetUserType`, user, { responseType: 'text' });
  }

  getCodesMasterList() : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/CodesMasterAPI/FetchCodesMaster`);
  }

  deleteCodesMaster(cmCode: string, cmType: string): Observable<string> {
    return this.http.delete(`${this.apiUrl}/CodesMasterAPI/DeleteCodesMaster?cmCode=${cmCode}&cmType=${cmType}`, { responseType: 'text' });
  }
  SaveCodesMaster(codesMasterEntity: CodesMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/CodesMasterAPI/SaveCodesMaster`, codesMasterEntity, { responseType: 'text' });
  }
  UpdateCodesMaster(codesMasterEntity: CodesMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/CodesMasterAPI/UpdateCodesMaster`, codesMasterEntity, { responseType: 'text' });
  }
  checkDuplicateCodesMaster(codesMasterEntity: CodesMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/CodesMasterAPI/CheckDuplicateCodesMaster`, codesMasterEntity, { responseType: 'text' });
  }
  getErrorCode(id: string) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/ErrorCodesMasterAPI/GetErrorCodes/${id}`);
  }
  getMotorClaimList() : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/MotorClaimAPI/FetchMotorClaimList`);
  }
  saveMotorClaim(motorClaimEntity: MotorClaimEntity): Observable<string> {
    console.log(motorClaimEntity);
    return this.http.post(`${this.apiUrl}/MotorClaimAPI/SaveMotorClaim`, motorClaimEntity, { responseType: 'text' });
  }
  updateMotorClaim(motorClaimEntity: MotorClaimEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/MotorClaimAPI/UpdateMotorClaim`, motorClaimEntity, { responseType: 'text' });
  }
  UpdateSurveyCreated(motorClaimEntity: MotorClaimEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/SurveyAPI/UpdateSurveyCreated`, motorClaimEntity, { responseType: 'text' });
  }
  updateApprovalSts(motorClaimEntity: MotorClaimEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/MotorClaimAPI/UpdateAppovalStatus`, motorClaimEntity, { responseType: 'text' });
  }
  checkDuplicateMotorClaim(clmPolRepNo: string): Observable<string> {
   
    return this.http.get(`${this.apiUrl}/MotorClaimAPI/CheckDuplicatePolRepNo/${clmPolRepNo}`,{ responseType: 'text' });
  }
  deleteMotorClaim(clmUid: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/MotorClaimAPI/DeleteMotorClaim?id=${clmUid}`, { responseType: 'text' });
  }
  getSurveyHistoryList() : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/SurveyAPI/FetchAllSurveyHistory`);
  }
  getErrorCodesMasterList() : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/ErrorCodesMasterAPI/FetchErrorCodesMaster`);
  }
  SaveErrorCodesMaster(errorMasterEntity: ErrorMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/ErrorCodesMasterAPI/SaveErrorCodesMaster`, errorMasterEntity, { responseType: 'text' });
  }
  UpdateErrorCodesMaster(errorMasterEntity: ErrorMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/ErrorCodesMasterAPI/UpdateErrorCodesMaster`, errorMasterEntity, { responseType: 'text' });
  }
  deleteErrorCodesMaster(errCode: string): Observable<string> {
    return this.http.delete(`${this.apiUrl}/ErrorCodesMasterAPI/DeleteErrorCodesMaster/${errCode}`, { responseType: 'text' });
  }
  checkDuplicateErrorCodesMaster(errCode: string): Observable<string> {
    return this.http.post(`${this.apiUrl}/ErrorCodesMasterAPI/CheckDuplicateErrorCodesMaster/${errCode}`, null, { responseType: 'text' });
  } 
  SaveUserMaster(userMasterEntity: UserMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/UserMasterAPI/SaveUserMaster`, userMasterEntity, { responseType: 'text' });
  }
  UpdateUserMaster(userMasterEntity: UserMasterEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/UserMasterAPI/UpdateUserMaster`, userMasterEntity, { responseType: 'text' });
  }
  DeleteUserMaster(userId: string): Observable<string> {
    return this.http.delete(`${this.apiUrl}/UserMasterAPI/DeleteUserMaster/${userId}`, { responseType: 'text' });
  }
  CheckDuplicateUserMaster(userId: string): Observable<string> {
    return this.http.post(`${this.apiUrl}/UserMasterAPI/CheckDuplicateUserMaster/${userId}`, null, { responseType: 'text' });
  } 
  getUserMasterList() : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/UserMasterAPI/FetchUserMaster`);
  }
  getSurveyHeaderByClmUid(clmUid:number) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/SurveyAPI/FetchByClmUid/${clmUid}`);
  }
  getSurveyHeaderBySurUid(surUid:number) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/SurveyAPI/FetchBySurUid/${surUid}`);
  }
  getSurveyHeaderBySurClmUid(surClmUid:number) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/SurveyAPI/FetchBySurClmUid/${surClmUid}`);
  }
  getCodes(cmCode: string) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/CodesMasterAPI/GetCodes/${cmCode}`);
  }
  saveSurveyHeader(motorClmSurHdrEntity: MotorClmSurHdrEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/SurveyAPI/SaveSurveyHeader`, motorClmSurHdrEntity, { responseType: 'text' });
  }
  updateSurveyHeader(motorClmSurHdrEntity: MotorClmSurHdrEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/SurveyAPI/UpdateSurveyHeader`, motorClmSurHdrEntity, { responseType: 'text' });
  }
  updateSurveyStatus(motorClmSurHdrEntity: MotorClmSurHdrEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/SurveyAPI/UpdateSurveyStatus`, motorClmSurHdrEntity, { responseType: 'text' });
  }
  getSurveyDetailsList(surdSurUid: number) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/SurveyAPI/FetchSurveyDetailsList/${surdSurUid}`);
  }
  GetSurUidSequence() : Observable<number>{
    return this.http.get<number>(`${this.apiUrl}/SurveyAPI/GetSurUidSequence`);
  }
  GetClaimUidSequence() : Observable<number>{
    return this.http.get<number>(`${this.apiUrl}/MotorClaimAPI/GetClaimSequence`);
  }
  GetSurdUidSequence() : Observable<number>{
    return this.http.get<number>(`${this.apiUrl}/SurveyAPI/GetSurdUidSequence`);
  }
  getMotorClaim(clmUid:number) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/MotorClaimAPI/FetchMotorClaim/${clmUid}`);
  }
  getSurveyList(userId: string) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/SurveyAPI/FetchSurveyHeaderList/${userId}`);
  }
  saveSurveyDetails(motorClmSurDtlEntity: MotorClmSurDtlEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/SurveyAPI/SaveSurveyDetails`, motorClmSurDtlEntity, { responseType: 'text' });
  }
  updateSurveyDetails(motorClmSurDtlEntity: MotorClmSurDtlEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/SurveyAPI/UpdateSurveyDetails`, motorClmSurDtlEntity, { responseType: 'text' });
  }
  deleteSurveyDetails(surdUid: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/SurveyAPI/DeleteSurveyDetails/${surdUid}`, { responseType: 'text' });
  }
  checkDuplicateSurveyDetails(motorClmSurDtlEntity: MotorClmSurDtlEntity): Observable<string> {
    return this.http.post(`${this.apiUrl}/SurveyAPI/checkDuplicateSurveyDetails`, motorClmSurDtlEntity, { responseType: 'text' });
  }
}
