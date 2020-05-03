import { Injectable } from "@angular/core";
import { DataService } from "../shared/services/data.service";
import { ConfigurationService } from "../shared/services/configuration.service";
import { ICommentList } from "../shared/models/commentList.model";
import { Observable } from "rxjs/internal/Observable";
import { tap } from "rxjs/internal/operators/tap";

@Injectable()
export class CommentService {

    private catalogUrl: string = '';
    
    constructor(private service: DataService, private configurationService: ConfigurationService) {
        this.configurationService.settingsLoaded$.subscribe(x => {
            this.catalogUrl = this.configurationService.serverSettings.purchaseUrl + '/c/api/v1/catalog/items';
        });
    }

    getComments(itemId: number): Observable<ICommentList>{
        var url = this.catalogUrl + '/' + itemId + '/comments';
        return this.service.get(url)
        .pipe<ICommentList>(tap((response: any) => {
            return response;
        }));
    }
}